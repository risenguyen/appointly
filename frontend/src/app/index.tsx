import { routeTree } from "@/routeTree.gen";
import { createRouter, RouterProvider } from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";

import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";

import { client } from "../api/client.gen";
import { configureClient } from "../lib/configure-client";

// Configures the API client
configureClient(client);

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 1,
    },
  },
});

const router = createRouter({
  routeTree,
  defaultPreload: "intent",
  defaultPreloadStaleTime: 0,
  defaultPendingMs: 0,
  scrollRestoration: true,
  context: {
    queryClient: queryClient, // Inject queryClient
  },
});

// Register the router instance for type safety
declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <ReactQueryDevtools />
      <TanStackRouterDevtools router={router} />
      <RouterProvider router={router} />
    </QueryClientProvider>
  );
}

export default App;
