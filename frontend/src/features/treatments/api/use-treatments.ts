import { queryOptions, useSuspenseQuery } from "@tanstack/react-query";
import { getApiTreatments, GetApiTreatmentsData, Options } from "@/api";

function treatmentsQueryOptions(
  fetchOptions?: Options<GetApiTreatmentsData, true>,
) {
  return queryOptions({
    queryFn: async () => {
      const { data } = await getApiTreatments({
        throwOnError: true,
        ...fetchOptions,
      });
      return data;
    },
    queryKey: ["treatments", fetchOptions],
    staleTime: 1000 * 60 * 30,
  });
}

function useTreatments({
  queryOptions,
  fetchOptions,
}: {
  queryOptions?: Omit<
    ReturnType<typeof treatmentsQueryOptions>,
    "queryFn" | "queryKey"
  >;
  fetchOptions?: Options<GetApiTreatmentsData, true>;
} = {}) {
  return useSuspenseQuery({
    ...treatmentsQueryOptions(fetchOptions),
    ...queryOptions,
  });
}

export { useTreatments, treatmentsQueryOptions };
