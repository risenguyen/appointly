import {
  useMutation,
  useQueryClient,
  type MutationOptions,
  type DefaultError,
} from "@tanstack/react-query";

import { deleteApiStaffById, type ProblemDetails } from "@/api";

import { staffQueryOptions } from "./use-staff";

function useDeleteStaff(
  options: Omit<
    MutationOptions<void, DefaultError | ProblemDetails, number>,
    "mutationFn"
  > = {},
) {
  const queryClient = useQueryClient();
  const { onSuccess, ...restOptions } = options;
  return useMutation({
    mutationKey: ["deletestaff"],
    mutationFn: async (id) => {
      await deleteApiStaffById({
        path: {
          id,
        },
        throwOnError: true,
      });
    },
    onSuccess: (data, id, context) => {
      queryClient.setQueryData(staffQueryOptions().queryKey, (staff) => {
        return staff?.filter((staffMember) => staffMember.id !== id);
      });
      onSuccess?.(data, id, context);
    },
    ...restOptions,
  });
}

export { useDeleteStaff };
