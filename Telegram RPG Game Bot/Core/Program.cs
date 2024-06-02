using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Core
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Bot.Start(UpdateHandler, ErrorHandler);

            Console.ReadLine();
            
            Bot.Stop();

            Console.ReadLine();
        }

        private static async Task UpdateHandler(ITelegramBotClient bot, Update update,
            CancellationToken cancellationToken)
        {
        }
        
        private static async Task ErrorHandler(ITelegramBotClient bot, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception);
        }
    }
}
