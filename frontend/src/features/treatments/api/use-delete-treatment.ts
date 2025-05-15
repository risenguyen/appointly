import {
  useMutation,
  useQueryClient,
  type MutationOptions,
  type DefaultError,
} from "@tanstack/react-query";

import { deleteApiTreatmentsById, type ProblemDetails } from "@/api";

import { treatmentsQueryOptions } from "./use-treatments";

function useDeleteTreatment(
  options: Omit<
    MutationOptions<unknown, DefaultError | ProblemDetails, number>,
    "mutationFn"
  > = {},
) {
  const queryClient = useQueryClient();
  const { onSuccess, ...restOptions } = options;
  return useMutation({
    mutationFn: async (id) => {
      await deleteApiTreatmentsById({
        path: {
          id,
        },
        throwOnError: true,
      });
    },
    onSuccess: (data, variables, context) => {
      queryClient.invalidateQueries(treatmentsQueryOptions());
      onSuccess?.(data, variables, context);
    },
    ...restOptions,
  });
}

export { useDeleteTreatment };
