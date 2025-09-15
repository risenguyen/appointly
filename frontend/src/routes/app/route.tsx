import { createFileRoute, Outlet, redirect } from "@tanstack/react-router";
import AppLayout from "./-layouts/app-layout";
import { toast } from "sonner";

export const Route = createFileRoute("/app")({
  component: RouteComponent,
  beforeLoad({ context: { auth } }) {
    if (!auth?.isAuthenticated) {
      toast("Session expired. Please log in again.");
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
