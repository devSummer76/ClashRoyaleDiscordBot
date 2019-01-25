using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Service;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace ClashRoyaleBot {
        public class Program {

            private CommandService commands;
            private DiscordSocketClient client;
            private IServiceProvider services;

            private string name = System.Reflection.Assembly.GetEntryAssembly().GetName().ToString();
            private string version = string.Format("Version: {0}", System.Reflection.Assembly.GetEntryAssembly().GetName().Version);

            static void Main(string[] args)
                => new Program().Start().GetAwaiter().GetResult();

            public async Task Start() {
                await Log(new LogMessage(LogSeverity.Info, name, version));

                // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //                       CONFIGURATION

                string token = "12345678901234567890123456789012345678901234567890123456789012345678";
                string clashRoyale_ApiKey = "api key here";
                // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


                // When working with events that have Cacheable<IMessage, ulong> parameters,
                // you must enable the message cache in your config settings if you plan to
                // use the cached message entity.
                client = new DiscordSocketClient(new DiscordSocketConfig {
                    LogLevel = LogSeverity.Debug,
                    MessageCacheSize = 100
                });

                client.Log += Log;

                commands = new CommandService();


                services = new ServiceCollection()
                    //.AddSingleton(new NotificationService())
                    .AddSingleton(new ApiService(clashRoyale_ApiKey))
                        .BuildServiceProvider();

                await InstallCommands();

                await client.LoginAsync(TokenType.Bot, token);
                await client.StartAsync();

                client.MessageUpdated += MessageUpdated;

                client.Ready += () => {
                    Console.WriteLine("Bot is connected!");
                    return Task.CompletedTask;
                };

                await Task.Delay(-1);
            }
            private Task Log(LogMessage message) {
                Console.WriteLine(message.ToString());
                return Task.CompletedTask;
            }

            private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel) {
                // If the message was not in the cache, downloading it will result in getting a copy of `after`.
                var message = await before.GetOrDownloadAsync();
                Console.WriteLine($"{message} -> {after}");
            }

            public async Task InstallCommands() {
                // Hook the MessageReceived Event into our Command Handler
                client.MessageReceived += HandleCommand;

                // Discover all of the commands in this assembly and load them.
                await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);
            }

            public async Task HandleCommand(SocketMessage messageParam) {
                // Don't process the command if it was a System Message
                var message = messageParam as SocketUserMessage;
                if (message == null)
                    return;
                // Create a number to track where the prefix ends and the command begins
                int argPos = 0;
                // Determine if the message is a command, based on if it starts with '!' or a mention prefix
                if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos)))
                    return;
                // Create a Command Context
                var context = new CommandContext(client, message);
                // Execute the command. (result does not indicate a return value, 
                // rather an object stating if the command executed successfully)
                var result = await commands.ExecuteAsync(context, argPos, services);
                if (!result.IsSuccess)
                    await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
}
