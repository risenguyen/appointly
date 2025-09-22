import { z } from "zod";

import {
  useQueryClient,
  useMutation,
  type MutationOptions,
  type DefaultError,
} from "@tanstack/react-query";

import {
  putApiStaffById,
  type ValidationProblemDetails,
  type StaffResponse,
  type ProblemDetails,
} from "@/api";

import { staffQueryOptions } from "./use-staff";

type EditStaffInput = z.infer<typeof editStaffSchema>;

const editStaffSchema = z.object({
  id: z.number(),
  firstName: z
    .string()
    .min(1, "First name is required.")
    .max(50, "First Name cannot exceed 50 characters."),
  lastName: z
    .string()
    .min(1, "Last name is required.")
    .max(50, "Last Name cannot exceed 50 characters."),
  email: z.string().email("Invalid email address."),
  phone: z.string().regex(/^\d{10}$/, "Phone number must be 10 digits."),
});

function useEditStaff(
  options: Omit<
    MutationOptions<
      StaffResponse,
      DefaultError | ProblemDetails | ValidationProblemDetails,
      EditStaffInput
    >,
    "mutationFn"
  > = {},
) {
  const queryClient = useQueryClient();
  const { onSuccess, ...restOptions } = options;
  return useMutation({
    mutationFn: async (formData) => {
      const { data } = await putApiStaffById({
        path: {
          id: formData.id,
        },
        body: formData,
        throwOnError: true,
      });
      return data;
    },
    onSuccess: (data, variables, context) => {
      queryClient.setQueryData(staffQueryOptions().queryKey, (staff) => {
        if (staff) {
          return staff.map((staffMember) =>
            staffMember.id === data.id ? data : staffMember,
          );
        }
        return [data];
      });
      onSuccess?.(data, variables, context);
    },
    ...restOptions,
  });
}

export { useEditStaff, editStaffSchema, type EditStaffInput };
