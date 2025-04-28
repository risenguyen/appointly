import { type GetApiTreatmentsByIdError } from "@/api";
import { createFileRoute } from "@tanstack/react-router";
import { type TypedErrorComponentProps } from "@/types/tanstack-router";

export const Route = createFileRoute("/")({
  component: RouteComponent,
  async loader() {},
  errorComponent({
    error,
  }: TypedErrorComponentProps<GetApiTreatmentsByIdError>) {
    return <div>{error.status}</div>;
  },
});

function RouteComponent() {
  return (
    <div>
      Hello "/"!
      <button type="button">Click me</button>
    </div>
  );
}
