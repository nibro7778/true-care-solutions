using Common;

namespace Clients;

// Clients will be replaced with the name of the module
public class ClientsModule() : BaseModule(CompositionRoot.BeginLifetimeScope), IClients;