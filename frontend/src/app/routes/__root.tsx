import { Outlet, createRootRouteWithContext } from "@tanstack/react-router";
import { handleRootErrorComponent } from "@/lib/tanstack-router";
import { QueryClient } from "@tanstack/react-query";

type RouterContext = {
  queryClient: QueryClient;
};

export const Route = createRootRouteWithContext<RouterContext>()({
  component: RootComponent,
  errorComponent: (props) =>
    handleRootErrorComponent({
      errorComponent: ({ error }) => {
        console.log(error);
        return (
          <div className="flex h-screen w-screen flex-col items-center justify-center px-6">
            <h1 className="text-center text-3xl font-bold lg:text-5xl">
              Something Went Wrong
            </h1>
            <p className="text-primary mt-1 text-center text-base lg:mt-3 lg:text-xl">
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
