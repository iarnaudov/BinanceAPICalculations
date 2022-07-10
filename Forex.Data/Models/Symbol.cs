using System.ComponentModel.DataAnnotations;

namespace Forex.Data.Models
{
    public class Symbol
    {
        [Key]
        public int Id { get; set; }
        public string SymbolName { get; set; }
    }
}
