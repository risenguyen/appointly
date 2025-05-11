import { queryOptions, useSuspenseQuery } from "@tanstack/react-query";
import { getApiTreatments } from "@/api";

function treatmentsQueryOptions() {
  return queryOptions({
    queryFn: async () => {
      const { data } = await getApiTreatments({
        throwOnError: true,
      });
      return data;
    },
    queryKey: ["treatments"],
  });
}

function useTreatments(options: ReturnType<typeof treatmentsQueryOptions>) {
  return useSuspenseQuery({
    ...treatmentsQueryOptions(),
    ...options,
  });
}

export { useTreatments, treatmentsQueryOptions };
