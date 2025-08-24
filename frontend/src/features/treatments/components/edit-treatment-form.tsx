import { useCallback } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { toast } from "sonner";

import {
  useEditTreatment,
  editTreatmentSchema,
  type EditTreatmentInput,
} from "../api/use-edit-treatment";
import type { TreatmentResponse } from "@/api";

import { LoaderCircle } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";

type EditTreatmentFormProps = {
  treatment: TreatmentResponse;
  setOpen: (open: boolean) => void;
};

function EditTreatmentForm({ treatment, setOpen }: EditTreatmentFormProps) {
  const editTreatment = useEditTreatment({
    onSuccess: () => {
      toast.success("Treatment edited successfully.");
      setOpen(false);
    },
    onError: (error) => {
      toast.error(
        `Failed to edit treatment. ${error instanceof Error ? "Something went wrong." : `(${error.status})`}`,
      );
      setOpen(false);
    },
  });

  const form = useForm<EditTreatmentInput>({
    resolver: zodResolver(editTreatmentSchema),
    defaultValues: {
      id: treatment.id,
      name: treatment.name,
      description: treatment.description,
      price: treatment.price.toString(),
      durationInMinutes: treatment.durationInMinutes.toString(),
      treatmentType: treatment.treatmentType,
    },
  });

  const onSubmit = useCallback(
    (data: EditTreatmentInput) => editTreatment.mutate(data),
    [editTreatment],
  );

  return (
    <Form {...form}>
      <form className="space-y-6" onSubmit={form.handleSubmit(onSubmit)}>
        <FormField
          control={form.control}
          name="name"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Name</FormLabel>
              <FormControl>
                <Input placeholder="Men's Classic Cut" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="description"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Description</FormLabel>
              <FormControl>
                <Input
                  placeholder="Includes consultation, shampoo, cut, rinse, and basic style."
                  {...field}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="price"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Price</FormLabel>
              <FormControl>
                <Input placeholder="$45.00" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="durationInMinutes"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Duration In Minutes</FormLabel>
              <FormControl>
                <Input type="text" placeholder="30" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="treatmentType"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Treatment Type</FormLabel>
              <Select
                defaultValue={treatment.treatmentType.toString()}
                onValueChange={field.onChange}
              >
                <FormControl>
                  <SelectTrigger>
                    <SelectValue placeholder="Select a Treatment Type" />
                  </SelectTrigger>
                </FormControl>
                <SelectContent autoFocus>
                  <SelectItem value="0">Hair</SelectItem>
                  <SelectItem value="1">Nails</SelectItem>
                  <SelectItem value="2">Massage</SelectItem>
                </SelectContent>
              </Select>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button disabled={editTreatment.isPending} className="w-full">
          {editTreatment.isPending ? (
            <LoaderCircle className="animate-spin" />
          ) : null}
          Edit Treatment
        </Button>
      </form>
    </Form>
  );
}

export default EditTreatmentForm;
