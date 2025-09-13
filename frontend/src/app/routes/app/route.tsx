import { createFileRoute, Outlet, redirect } from "@tanstack/react-router";
import AppLayout from "./-layouts/app-layout";

export const Route = createFileRoute("/app")({
  component: RouteComponent,
  beforeLoad({ context: { auth } }) {
    if (!auth?.isAuthenticated) {
      throw redirect({
        to: "/login",
        search: {
          redirect: location.href,
        },
      });
    }
  },
});

function RouteComponent() {
  return (
    <AppLayout>
      <Outlet />
    </AppLayout>
  );
}
