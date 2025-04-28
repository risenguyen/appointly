import { type GetApiTreatmentsByIdError } from "@/api";
import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/")({
  component: RouteComponent,
  async loader() {},
  errorComponent({
    error,
    reset,
  }: {
    error: GetApiTreatmentsByIdError;
    reset: () => void;
  }) {
    console.log(error, reset);
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
