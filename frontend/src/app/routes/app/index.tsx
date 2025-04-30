import { createFileRoute } from "@tanstack/react-router";
import { handleErrorComponent } from "@/lib/tanstack-router";

import { useSuspenseQuery } from "@tanstack/react-query";
import { getApiTreatmentsById, type GetApiTreatmentsByIdError } from "@/api";

import { Button } from "@/components/ui/button";

export const Route = createFileRoute("/app/")({
  component: RouteComponent,
  pendingComponent() {
    return <div>Loading...</div>;
  },
  errorComponent: (props) =>
    handleErrorComponent<GetApiTreatmentsByIdError>({
      errorComponent: ({ error }) => <div>{error.status}</div>,
      props,
    }),
});

function RouteComponent() {
  const { data } = useSuspenseQuery({
    queryKey: ["treatments", 2],
    queryFn: () =>
      getApiTreatmentsById({
        path: {
          id: 2,
        },
        throwOnError: true,
      })
        .then(({ data }) => data)
        .catch((error) => {
          throw error;
        }),
  });

  console.log(data);

  return (
    <div>
      Hello "/app/"!
      <Button>First Ever Button</Button>
    </div>
  );
}
