import { Outlet, createRootRouteWithContext } from "@tanstack/react-router";
import { QueryClient } from "@tanstack/react-query";
import { handleRootErrorComponent } from "@/lib/tanstack-router";

type RouterContext = {
  queryClient: QueryClient;
};

export const Route = createRootRouteWithContext<RouterContext>()({
  component: RootComponent,
  errorComponent: (props) =>
    handleRootErrorComponent({
      errorComponent() {
        return (
          <div className="flex h-screen w-screen flex-col items-center justify-center bg-neutral-50 px-6">
            <h1 className="text-center text-3xl font-bold text-neutral-950 lg:text-5xl">
              Something Went Wrong
            </h1>
            <p className="mt-1 text-center text-base text-neutral-600 lg:mt-3 lg:text-xl">
              We encountered an unexpected issue. Please try refreshing the page
              or contact support if the problem persists.
            </p>
          </div>
        );
      },
      props,
    }),
});

function RootComponent() {
  return <Outlet />;
}
