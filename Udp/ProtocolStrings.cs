using System;

namespace Udp
{
    public static class ProtocolStrings
    {
        // Pole znacznika czasu
        public static string TimeField { get; } = "czas";

        // Pole identyfikatora sesji
        public static string SessionField { get; } = "iden";

        // Pole licznika komunikatów
        public static string CounterField { get; } = "licz";

        // Pole operacji
        public static string OperationField { get; } = "oper";

        // Prefiks pola argumentu liczbowego
        public static string ArgField { get; } = "num";

        // Pole liczby argumentów liczbowych
        public static string ArgCounterField { get; } = "nums";

        // Pole żądania
        public static string RequestField { get; } = "stat";

        // Pole odpowiedzi
        public static string ResponseField { get; } = "wiad";

        // Pole statusu operacji
        public static string StatusField { get; } = "powo";

        // Pole wyniku
        public static string ResultField { get; } = "dane";

        // Pole błędu
        public static string ErrorField { get; } = "blad";


        // Akcja powitania dla pola żądania (od klienta)
        public static string RequestFieldHelloAction { get; } = "helloserver";

        // Akcja powitania dla pola odpowiedzi (od serwera)
        public static string ResponseFieldHelloAction { get; } = "hellouser";

        // Akcja pożegnania dla pola żądania (od klienta)
        public static string RequestFieldGoodbyeAction { get; } = "byeserver";

        // Akcja pożegnania dla pola odpowiedzi (od serwera)
        public static string ResponseFieldGoodbyeAction { get; } = "byeuser";

        // Akcja żądania operacji (wyniku) dla pola żądania
        public static string RequestFieldOperationAction { get; } = "wynik";

        // Akcja zwracania wyniku dla pola odpowiedzi
        public static string ResponseFieldResultAction { get; } = "odpowiedz";

        // Wartość prawdziwa dla pola statusu operacji (mówi o tym, że operacja się powiodła)
        public static string StatusFieldTrueResult { get; } = "1";

        // Wartość fałszywa dla pola statusu operacji (mówi o tym, że operacja się nie powiodła)
        public static string StatusFieldFalseResult { get; } = "0";
    }
}
