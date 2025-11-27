using System;

namespace TarjetaSube
{
    public class Boleto
    {
        public string LineaColectivo { get; private set; }
        public decimal ImportePagado { get; private set; }
        public decimal SaldoRestante { get; private set; }
        public DateTime FechaHora { get; private set; }
        public bool EsTransbordo { get; private set; }
        public long TarjetaId { get; private set; }
        public string TipoTarjeta { get; private set; }

        public Boleto(string linea, decimal importe, Tarjeta tarjeta, bool esTransbordo = false)
        {
            LineaColectivo = linea;
            ImportePagado = importe;
            SaldoRestante = tarjeta.ObtenerSaldo();
            FechaHora = DateTime.Now;
            EsTransbordo = esTransbordo;
            TarjetaId = tarjeta.ObtenerId();

            // ← TIPO DE TARJETA (lo que faltaba en la consigna)
            TipoTarjeta = tarjeta switch
            {
                TarjetaMedioBoleto => "Medio Boleto Estudiantil",
                TarjetaBoletoGratuito => "Boleto Educativo Gratuito",
                TarjetaFranquiciaCompleta => "Franquicia Completa (Jubilados/Discapacidad)",
                _ => "Tarjeta Normal"
            };
        }

        public void MostrarInformacion()
        {
            Console.WriteLine("================================");
            Console.WriteLine("      BOLETO DE COLECTIVO      ");
            Console.WriteLine("================================");
            Console.WriteLine($"Línea: {LineaColectivo}");
            Console.WriteLine($"Tarjeta ID: {TarjetaId}");
            Console.WriteLine($"Tipo de tarjeta: {TipoTarjeta}");
            Console.WriteLine($"Fecha: {FechaHora:dd/MM/yyyy}");
            Console.WriteLine($"Hora: {FechaHora:HH:mm:ss}");
            Console.WriteLine($"Importe pagado: ${ImportePagado:N0}");
            Console.WriteLine($"Saldo restante: ${SaldoRestante:N0}");

            if (EsTransbordo)
                Console.WriteLine("*** TRASBORDO GRATUITO ***");

            if (ImportePagado == 0m && !EsTransbordo)
                Console.WriteLine("*** VIAJE GRATUITO ***");

            Console.WriteLine("================================");
        }

        public string ObtenerTipoTarjeta() => TipoTarjeta;
        public long ObtenerIdTarjeta() => TarjetaId;
        public decimal ObtenerTotalAbonado() => ImportePagado;
    }
}