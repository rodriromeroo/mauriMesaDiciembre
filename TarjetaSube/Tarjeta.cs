using System;
using System.Collections.Generic;

namespace TarjetaSube
{
    public class Tarjeta
    {
        protected decimal saldo;
        private decimal saldoPendiente;
        private const decimal LIMITE_NEGATIVO = -1200m;
        private const decimal LIMITE_MAXIMO = 56000m;
        private readonly List<decimal> montosPermitidos;

        // ID único de tarjeta
        private readonly long id;
        private static long contadorId = 1000000;
        public long Id => id;

        // Uso frecuente
        protected int viajesDelMes;
        protected DateTime ultimoMesRegistrado;

        // Trasbordo
        private DateTime ultimoViajeFechaHora = DateTime.MinValue;
        private string ultimaLineaViajada = "";

        public Tarjeta()
        {
            id = ++contadorId;
            saldo = 0;
            saldoPendiente = 0;
            montosPermitidos = new List<decimal>
            {
                2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000
            };
            viajesDelMes = 0;
            ultimoMesRegistrado = DateTime.Now;
        }

        public decimal ObtenerSaldo() => saldo;
        public decimal ObtenerSaldoPendiente() => saldoPendiente;
        public long ObtenerId() => id;

        public int ObtenerViajesDelMes()
        {
            DateTime ahora = DateTime.Now;
            if (ahora.Month != ultimoMesRegistrado.Month || ahora.Year != ultimoMesRegistrado.Year)
            {
                viajesDelMes = 0;
                ultimoMesRegistrado = ahora;
            }
            return viajesDelMes;
        }

        public decimal CalcularDescuentoUsoFrecuente(decimal montoBase)
        {
            DateTime ahora = DateTime.Now;
            if (ahora.Month != ultimoMesRegistrado.Month || ahora.Year != ultimoMesRegistrado.Year)
            {
                viajesDelMes = 0;
                ultimoMesRegistrado = ahora;
            }

            viajesDelMes++;

            if (viajesDelMes >= 1 && viajesDelMes <= 29) return montoBase;
            else if (viajesDelMes <= 59) return montoBase * 0.80m;
            else if (viajesDelMes <= 80) return montoBase * 0.75m;
            else return montoBase;
        }

        public bool CargarSaldo(decimal monto)
        {
            if (!montosPermitidos.Contains(monto)) return false;

            if (saldoPendiente > 0) AcreditarCarga();

            decimal nuevoSaldo = saldo + monto;
            if (nuevoSaldo > LIMITE_MAXIMO)
            {
                decimal excedente = nuevoSaldo - LIMITE_MAXIMO;
                saldo = LIMITE_MAXIMO;
                saldoPendiente += excedente;
                return true;
            }

            saldo = nuevoSaldo;
            return true;
        }

        public void AcreditarCarga()
        {
            if (saldoPendiente <= 0) return;
            decimal espacio = LIMITE_MAXIMO - saldo;
            if (espacio <= 0) return;

            decimal aAcreditar = Math.Min(saldoPendiente, espacio);
            saldo += aAcreditar;
            saldoPendiente -= aAcreditar;
        }

        public virtual bool DescontarSaldo(decimal monto)
        {
            decimal resultado = saldo - monto;
            if (resultado < LIMITE_NEGATIVO) return false;

            saldo = resultado;
            if (saldoPendiente > 0) AcreditarCarga();
            return true;
        }

        public virtual decimal ObtenerMontoAPagar(decimal tarifaBase) => tarifaBase; // polimorfismo implementado!!
        public virtual bool PuedeUsarseAhora() => true;
        public virtual void RegistrarViaje() { }

        public void RegistrarUltimoViaje(string linea, DateTime cuando)
        {
            ultimaLineaViajada = linea;
            ultimoViajeFechaHora = cuando;
        }

        public bool EsTrasbordoValido(string nuevaLinea, DateTime ahora)
        {
            if (ultimoViajeFechaHora == DateTime.MinValue) return false;
            if (string.Equals(ultimaLineaViajada, nuevaLinea, StringComparison.OrdinalIgnoreCase)) return false;
            if ((ahora - ultimoViajeFechaHora).TotalMinutes > 60) return false;
            if (ahora.DayOfWeek == DayOfWeek.Sunday) return false;
            if (ahora.Hour < 7 || ahora.Hour >= 22) return false;
            return true;
        }
    }
}