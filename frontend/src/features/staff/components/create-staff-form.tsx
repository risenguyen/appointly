import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { toast } from "sonner";

import {
  useCreateStaff,
  createStaffSchema,
  type CreateStaffInput,
} from "../api/use-create-staff";

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

type CreateStaffFormProps = {
  setOpen: (open: boolean) => void;
};

function CreateStaffForm({ setOpen }: CreateStaffFormProps) {
  const createStaff = useCreateStaff({
    onSuccess: () => {
      toast.success("Staff created successfully.");
      setOpen(false);
    },
    onError: (error) => {
      toast.error(
        `Failed to create staff. ${error instanceof Error ? "Something went wrong." : `(${error.status})`}`,
      );
      setOpen(false);
    },
  });

  const form = useForm<CreateStaffInput>({
    resolver: zodResolver(createStaffSchema),
    defaultValues: {
      firstName: "",
      lastName: "",
      email: "",
      phone: "",
    },
  });

  function onSubmit(data: CreateStaffInput) {
    createStaff.mutate(data);
  }

  return (
    <Form {...form}>
      <form className="space-y-6" onSubmit={form.handleSubmit(onSubmit)}>
        <FormField
          control={form.control}
          name="firstName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>First Name</FormLabel>
              <FormControl>
                <Input placeholder="John" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="lastName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Last Name</FormLabel>
              <FormControl>
                <Input placeholder="Smith" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="email"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Email</FormLabel>
              <FormControl>
                <Input
                  type="email"
                  placeholder="john.smith@example.com"
                  {...field}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="phone"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Phone Number</FormLabel>
              <FormControl>
                <Input type="tel" placeholder="2045678907" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button disabled={createStaff.isPending} className="w-full">
          {createStaff.isPending ? (
            <LoaderCircle className="animate-spin" />
          ) : null}
          Create Staff
        </Button>
      </form>
    </Form>
  );
}

export default CreateStaffForm;
