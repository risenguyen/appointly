import { routeTree } from "@/routeTree.gen";
import { createRouter, RouterProvider } from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";

import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";

import { ThemeContextProvider } from "@/context/theme-context";
import { useAuth, AuthContextProvider } from "@/context/auth-context";

import { client } from "@/api/client.gen";
import { configureClient } from "@/lib/configure-client";

import { Toaster } from "@/components/ui/sonner";

// Configure API client
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
    queryClient,
    auth: undefined,
  },
});

// Register the router instance for type safety
declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}

function InnerApp() {
  const auth = useAuth();

  return (
    <>
      <Toaster />
      <RouterProvider
        context={{
          queryClient,
          auth,
        }}
        router={router}
      />
      <ReactQueryDevtools />
      <TanStackRouterDevtools router={router} />
    </>
  );
}

function App() {
  return (
    <AuthContextProvider>
      <ThemeContextProvider>
        <QueryClientProvider client={queryClient}>
          <InnerApp />
        </QueryClientProvider>
      </ThemeContextProvider>
    </AuthContextProvider>
  );
}

export default App;
