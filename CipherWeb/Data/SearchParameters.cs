namespace CipherWeb.Data
{
    public class SearchParameter
    {
        public string Icon { get; set; }
        public string Label { get; set; }
        public List<string> Options { get; set; }
        public IEnumerable<string> BindValue { get; set; }
    }
}
