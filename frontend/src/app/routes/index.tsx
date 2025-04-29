import { createFileRoute, Link } from "@tanstack/react-router";

export const Route = createFileRoute("/")({
  component: RouteComponent,
  async loader() {},
});

function RouteComponent() {
  return (
    <div>
      Hello "/"!
      <Link to="/app">APP</Link>
    </div>
  );
}
