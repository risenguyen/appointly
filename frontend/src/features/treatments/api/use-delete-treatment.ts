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
    MutationOptions<void, DefaultError | ProblemDetails, number>,
    "mutationFn"
  > = {},
) {
  const queryClient = useQueryClient();
  const { onSuccess, ...restOptions } = options;
  return useMutation({
    mutationKey: ["deletetreatment"],
    mutationFn: async (id) => {
      await deleteApiTreatmentsById({
        path: {
          id,
        },
        throwOnError: true,
      });
    },
    onSuccess: (data, id, context) => {
      queryClient.setQueryData(
        treatmentsQueryOptions().queryKey,
        (treatments) => {
          return treatments?.filter((treatment) => treatment.id !== id);
        },
      );
      onSuccess?.(data, id, context);
    },
    ...restOptions,
  });
}

export { useDeleteTreatment };
