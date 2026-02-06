namespace PruebaBackendAPI.Models
{
    public class AlumnoRequest
    {
        public int AlumnoId { get; set; }
        public int MateriaId { get; set; }
        public int GradoId { get; set; }
        public decimal Nota { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
    }
}
