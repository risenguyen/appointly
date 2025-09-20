import { queryOptions, useSuspenseQuery } from "@tanstack/react-query";
import { getApiStaff } from "@/api";

function staffQueryOptions() {
  return queryOptions({
    queryFn: async () => {
      const { data } = await getApiStaff({
        throwOnError: true,
      });
      return data;
    },
    queryKey: ["staff"],
    staleTime: 1000 * 60 * 30,
  });
}

function useStaff({
  queryOptions,
}: {
  queryOptions?: Omit<
    ReturnType<typeof staffQueryOptions>,
    "queryFn" | "queryKey"
  >;
} = {}) {
  return useSuspenseQuery({
    ...staffQueryOptions(),
    ...queryOptions,
  });
}

export { useStaff, staffQueryOptions };
