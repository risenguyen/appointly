import { getApiTreatmentsById } from "@/api";
import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/")({
  component: RouteComponent,
  async loader() {
    const { data, error, request, response } = await getApiTreatmentsById({
      path: {
        id: 4,
      },
    });
    console.log(data);
    console.log(error);
    console.log(request);
    console.log(response);
  },
});

function RouteComponent() {
  return <div>Hello "/"!</div>;
}
