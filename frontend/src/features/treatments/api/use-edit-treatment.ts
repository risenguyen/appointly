import { z } from "zod";

import {
  useQueryClient,
  useMutation,
  type MutationOptions,
  type DefaultError,
} from "@tanstack/react-query";

import {
  putApiTreatmentsById,
  type ValidationProblemDetails,
  type TreatmentResponse,
  type ProblemDetails,
} from "@/api";

import { treatmentsQueryOptions } from "./use-treatments";

type EditTreatmentInput = z.infer<typeof editTreatmentSchema>;

const editTreatmentSchema = z.object({
  id: z.number(),
  name: z
    .string()
    .min(1, { message: "Treatment name is required." })
    .max(32, { message: "Treatment name cannot exceed 32 characters." }),
  description: z
    .string()
    .min(1, { message: "Treatment description is required." })
    .max(100, {
      message: "Treatment description cannot exceed 100 characters.",
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
  treatmentType: z.coerce.number({
    invalid_type_error: "Treatment type is required.",
  }),
});

function useEditTreatment(
  options: Omit<
    MutationOptions<
      TreatmentResponse,
      DefaultError | ProblemDetails | ValidationProblemDetails,
      EditTreatmentInput
    >,
    "mutationFn"
  > = {},
) {
  const queryClient = useQueryClient();
  const { onSuccess, ...restOptions } = options;
  return useMutation({
    mutationFn: async ({
      id,
      name,
      description,
      durationInMinutes,
      price,
      treatmentType,
    }) => {
      const { data } = await putApiTreatmentsById({
        path: {
          id,
        },
        body: {
          name,
          description,
          price: parseFloat(price.replace(/^\$/, "")),
          durationInMinutes: parseInt(durationInMinutes, 10),
          treatmentType,
        },
        throwOnError: true,
      });
      return data;
    },
    onSuccess: (data, variables, context) => {
      queryClient.setQueryData(
        treatmentsQueryOptions().queryKey,
        (treatments) => {
          if (treatments) {
            return treatments.map((treatment) =>
              treatment.id === data.id ? data : treatment,
            );
          }
          return [data];
        },
      );
      onSuccess?.(data, variables, context);
    },
    ...restOptions,
  });
}

export { useEditTreatment, editTreatmentSchema, type EditTreatmentInput };
