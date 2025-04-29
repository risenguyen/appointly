import { Outlet, createRootRouteWithContext } from "@tanstack/react-router";
import { QueryClient } from "@tanstack/react-query";

type RouterContext = {
  queryClient: QueryClient;
};

export const Route = createRootRouteWithContext<RouterContext>()({
  component: RootComponent,
  errorComponent: ({ error }) => {
    let heading = "Something Went Wrong";
    let description =
      "We encountered an unexpected issue. Please try refreshing the page or contact support if the problem persists.";
    console.log(error.message, error.name, error.stack);

    // Do a bunch of IF checks to determine the heading and description
    if (error.message.includes("Failed to fetch dynamically imported module")) {
      heading = "Connection Issue";
      description =
        " Part of the application could not be loaded. Please check your internet connection.";
    }

    return (
      <div className="flex h-screen w-screen flex-col items-center justify-center bg-neutral-50 px-6">
        <h1 className="text-center text-3xl font-bold text-neutral-950 lg:text-5xl">
          {heading}
        </h1>
        <p className="mt-1 text-center text-base text-neutral-600 lg:mt-3 lg:text-xl">
          {description}
        </p>
      </div>
    );
  },
});

function RootComponent() {
  return <Outlet />;
}
