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
    staleTime: 1000 * 60 * 30,
  });
}

function useTreatments(
  options: Omit<
    ReturnType<typeof treatmentsQueryOptions>,
    "queryFn" | "queryKey"
  > = {},
) {
  return useSuspenseQuery({
    ...treatmentsQueryOptions(),
    ...options,
  });
}

export { useTreatments, treatmentsQueryOptions };
