import { useCallback } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { toast } from "sonner";

import {
  useCreateTreatment,
  createTreatmentSchema,
  type CreateTreatmentInput,
} from "../api/use-create-treatment";

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

type CreateTreatmentFormProps = {
  setOpen: (open: boolean) => void;
};

function CreateTreatmentForm({ setOpen }: CreateTreatmentFormProps) {
  const createTreatment = useCreateTreatment({
    onSuccess: () => {
      toast.success("Treatment created successfully.");
      setOpen(false);
    },
    onError: (error) => {
      toast.error(
        `Failed to create treatment. ${error instanceof Error ? "Something went wrong." : `(${error.status})`}`,
      );
      setOpen(false);
    },
  });

  const form = useForm<CreateTreatmentInput>({
    resolver: zodResolver(createTreatmentSchema),
    defaultValues: {
      name: "",
      description: "",
      durationInMinutes: "",
      price: "",
    },
  });

  const onSubmit = useCallback(
    (data: CreateTreatmentInput) => createTreatment.mutate(data),
    [createTreatment],
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
                <Input type="number" placeholder="30" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button disabled={createTreatment.isPending} className="w-full">
          {createTreatment.isPending ? (
            <LoaderCircle className="animate-spin" />
          ) : null}
          Create Treatment
        </Button>
      </form>
    </Form>
  );
}

export default CreateTreatmentForm;
