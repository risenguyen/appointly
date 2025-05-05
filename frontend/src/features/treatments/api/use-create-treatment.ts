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
  durationInMinutes: z.coerce
    .string()
    .min(1, { message: "Treatment duration is required." })
    .regex(/^\d+$/, {
      message: "Treatment duration must be a whole number.",
    })
    .refine((val) => parseInt(val, 10) > 0, {
      message: "Treatment duration must be greater than 0.",
    }),
  price: z
    .string()
    .min(1, { message: "Treatment price is required." })
    .regex(/^\$?(\d+(\.\d{1,2})?)$/, {
      message:
        "Treatment price must be a valid positive monetary value (e.g., 10.99 or 50).",
    })
    .refine((val) => parseFloat(val.replace(/^\$/, "")) > 0, {
      message: "Treatment price must be greater than 0.",
    }),
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
    mutationFn: async ({ name, description, durationInMinutes, price }) => {
      const { data } = await postApiTreatments({
        body: {
          name,
          description,
          durationInMinutes: parseInt(durationInMinutes, 10),
          price: parseFloat(price.replace(/^\$/, "")),
        },
        throwOnError: true,
      });

      return data;
    },
    ...options,
  });
}

export { useCreateTreatment, createTreatmentSchema, type CreateTreatmentInput };
