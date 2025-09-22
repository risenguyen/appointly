import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { toast } from "sonner";

import {
  useEditStaff,
  editStaffSchema,
  type EditStaffInput,
} from "../api/use-edit-staff";
import type { StaffResponse } from "@/api";

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

type EditStaffFormProps = {
  staff: StaffResponse;
  setOpen: (open: boolean) => void;
};

function EditStaffForm({ staff, setOpen }: EditStaffFormProps) {
  const editStaff = useEditStaff({
    onSuccess: () => {
      toast.success("Staff member edited successfully.");
      setOpen(false);
    },
    onError: (error) => {
      toast.error(
        `Failed to edit staff member. ${error instanceof Error ? "Something went wrong." : `(${error.status})`}`,
      );
      setOpen(false);
    },
  });

  const form = useForm<EditStaffInput>({
    resolver: zodResolver(editStaffSchema),
    defaultValues: {
      id: staff.id,
      firstName: staff.firstName,
      lastName: staff.lastName,
      email: staff.email || "",
      phone: staff.phone || "",
    },
  });

  function onSubmit(data: EditStaffInput) {
    editStaff.mutate(data);
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
                <Input placeholder="john.smith@example.com" {...field} />
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
              <FormLabel>Phone</FormLabel>
              <FormControl>
                <Input type="tel" placeholder="2045678907" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button disabled={editStaff.isPending} className="w-full">
          {editStaff.isPending ? (
            <LoaderCircle className="animate-spin" />
          ) : null}
          Edit Staff Member
        </Button>
      </form>
    </Form>
  );
}

export default EditStaffForm;
