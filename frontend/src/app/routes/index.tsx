import { type GetApiTreatmentsByIdError } from "@/api";
import {
  createFileRoute,
  type ErrorComponentProps,
} from "@tanstack/react-router";

export const Route = createFileRoute("/")({
  component: RouteComponent,
  async loader() {},
  errorComponent({
    error,
    reset,
  }: Omit<ErrorComponentProps, "error"> & {
    error: GetApiTreatmentsByIdError;
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
