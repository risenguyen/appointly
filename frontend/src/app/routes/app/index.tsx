import { createFileRoute } from "@tanstack/react-router";
// import { handleErrorComponent } from "@/lib/tanstack-router";

import { Button } from "@/components/ui/button";

export const Route = createFileRoute("/app/")({
  component: RouteComponent,
  pendingComponent() {
    return <div>Loading...</div>;
  },
  // errorComponent: (props) =>
  //   handleErrorComponent<GetApiTreatmentsByIdError>({
  //     errorComponent: ({ error }) => <div>{error.status}</div>,
  //     props,
  //   }),
});

function RouteComponent() {
  return (
    <div>
      Hello "/app/"!
      <Button>First Ever Button</Button>
    </div>
  );
}
