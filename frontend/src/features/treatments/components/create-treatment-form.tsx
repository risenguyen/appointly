import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";

import {
  useCreateTreatment,
  createTreatmentSchema,
  type CreateTreatmentInput,
} from "../api/use-create-treatment";

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

function CreateTreatmentForm() {
  const createTreatment = useCreateTreatment({
    onError: (error) => {
      if (error instanceof Error) {
        throw error;
      }
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

  function onSubmit(data: CreateTreatmentInput) {
    createTreatment.mutate(data);
  }

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
                <Input placeholder="30" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button className="w-full">Create treatment</Button>
      </form>
    </Form>
  );
}

export default CreateTreatmentForm;
