﻿namespace CipherData.ApiMode
{
    public class ProcessDefinitionRequest : CipherClass, IProcessDefinitionRequest
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        public List<IProcessStepDefinition> Steps { get; set; } = new();
    }
}
