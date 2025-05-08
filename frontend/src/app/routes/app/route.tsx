import { createFileRoute, Outlet } from "@tanstack/react-router";
import AppLayout from "./-layouts/app-layout";

export const Route = createFileRoute("/app")({
  component: RouteComponent,
});

function RouteComponent() {
  return (
    <AppLayout>
      <Outlet />
    </AppLayout>
  );
}
