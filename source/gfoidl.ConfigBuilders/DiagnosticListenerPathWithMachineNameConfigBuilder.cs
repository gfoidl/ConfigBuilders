namespace gfoidl.ConfigBuilders
{
    public class DiagnosticListenerPathWithMachineNameConfigBuilder : DiagnosticListenerPathConfigBuilder
    {
        public override string ModifyPath(string path) => path.InsertMachineName();
    }
}
