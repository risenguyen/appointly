import {
  useMutation,
  type MutationOptions,
  type DefaultError,
} from "@tanstack/react-query";
import { z } from "zod";
import {
  postApiTreatments,
  type ValidationProblemDetails,
  type TreatmentResponse,
} from "@/api";

type CreateTreatmentInput = z.infer<typeof createTreatmentSchema>;

const createTreatmentSchema = z.object({
  name: z
    .string()
    .min(1, { message: "Treatment name is required." })
    .max(100, { message: "Treatment name cannot exceed 100 characters." }),
  description: z
    .string()
    .min(1, { message: "Treatment description is required." })
    .max(500, {
      message: "Treatment description cannot exceed 500 characters.",
    }),
  durationInMinutes: z
    .number()
    .positive({ message: "Duration must be a positive number of minutes." }),
  price: z.number().nonnegative({ message: "Price cannot be negative." }),
});

function useCreateTreatment(
  options?: Omit<
    MutationOptions<
      TreatmentResponse,
      DefaultError | ValidationProblemDetails,
      CreateTreatmentInput
    >,
    "mutationFn"
  >,
) {
  return useMutation({
    mutationFn: async (treatmentData) => {
      const { data } = await postApiTreatments({
        body: treatmentData,
        throwOnError: true,
      });
      return data;
    },
    ...options,
  });
}

export { useCreateTreatment, createTreatmentSchema, type CreateTreatmentInput };
