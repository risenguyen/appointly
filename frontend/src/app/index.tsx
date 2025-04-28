import { routeTree } from "@/routeTree.gen";
import { createRouter, RouterProvider } from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { client } from "../api/client.gen";

// Configure the API client
client.setConfig({
  baseUrl: import.meta.env.VITE_API_URL,
});

const queryClient = new QueryClient();

const router = createRouter({
  routeTree,
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
      <TanStackRouterDevtools router={router} />
      <RouterProvider router={router} />
    </QueryClientProvider>
  );
}

export default App;
