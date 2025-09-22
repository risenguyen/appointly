import { z } from "zod";

import {
  useQueryClient,
  useMutation,
  type MutationOptions,
  type DefaultError,
} from "@tanstack/react-query";

import {
  postApiStaff,
  type ValidationProblemDetails,
  type StaffResponse,
} from "@/api";
import { staffQueryOptions } from "./use-staff";

type CreateStaffInput = z.infer<typeof createStaffSchema>;

const createStaffSchema = z.object({
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

function useCreateStaff(
  options: Omit<
    MutationOptions<
      StaffResponse,
      DefaultError | ValidationProblemDetails,
      CreateStaffInput
    >,
    "mutationFn"
  > = {},
) {
  const queryClient = useQueryClient();
  const { onSuccess, ...restOptions } = options;

  return useMutation({
    mutationFn: async (inputData) => {
      const { data } = await postApiStaff({
        body: inputData,
        throwOnError: true,
      });
      return data;
    },
    onSuccess: (data, variables, context) => {
      queryClient.setQueryData(staffQueryOptions().queryKey, (staff) => {
        if (staff) {
          return [...staff, data];
        }
        return [data];
      });
      onSuccess?.(data, variables, context);
    },
    ...restOptions,
  });
}

export { useCreateStaff, createStaffSchema, type CreateStaffInput };
