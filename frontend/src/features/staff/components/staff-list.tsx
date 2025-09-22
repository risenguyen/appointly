import { useState } from "react";
import { toast } from "sonner";

import { useStaff } from "../api/use-staff";
import { useDeleteStaff } from "../api/use-delete-staff";
import type { StaffResponse } from "@/api";

import { Ellipsis, LoaderCircle, SquarePen, Trash2 } from "lucide-react";
import { Button } from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuTrigger,
  DropdownMenuContent,
  DropdownMenuItem,
} from "@/components/ui/dropdown-menu";
import {
  AlertDialog,
  AlertDialogContent,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogCancel,
} from "@/components/ui/alert-dialog";
import DrawerDialog from "@/components/shared/drawer-dialog";
import EditStaffForm from "./edit-staff-form";

function formatPhoneNumber(phoneNumber: string) {
  const cleaned = phoneNumber.replace(/\D/g, "");
  if (cleaned.length === 10) {
    return cleaned.replace(/(\d{3})(\d{3})(\d{4})/, "($1) $2-$3");
  }
  return phoneNumber;
}

function StaffItem({ staffMember }: { staffMember: StaffResponse }) {
  const [deleteDialogOpen, setDeleteDialogOpen] = useState(false);
  const [editDialogOpen, setEditDialogOpen] = useState(false);

  const deleteStaff = useDeleteStaff({
    onSuccess() {
      toast.success("Staff member deleted successfully.");
      setDeleteDialogOpen(false);
    },
    onError(error) {
      toast.error(
        `Failed to delete staff member. ${error instanceof Error ? "Something went wrong." : `(${error.status})`}`,
      );
      setDeleteDialogOpen(false);
    },
  });

  return (
    <li className="bg-card-2 relative flex aspect-[1.9] w-full flex-col justify-between rounded-md p-6 md:aspect-[1.72] lg:aspect-[1.49] xl:aspect-[2] 2xl:aspect-[2.38]">
      <div className="flex flex-col gap-0.5">
        <h1 className="text-base font-medium">
          {staffMember.firstName} {staffMember.lastName}
        </h1>
        <p className="text-muted-foreground text-base break-words">
          {staffMember.email}
        </p>
      </div>

      <div className="flex items-center justify-between">
        <span className="text-muted-foreground">
          {staffMember.phone ? formatPhoneNumber(staffMember.phone) : null}
        </span>
      </div>

      <DropdownMenu>
        <DropdownMenuTrigger asChild>
          <button
            aria-label={`More options for ${staffMember.firstName} ${staffMember.lastName}`}
            className="absolute top-6 right-6 focus:outline-none"
            type="button"
          >
            <Ellipsis
              className="cursor-pointer"
              size="16px"
              aria-hidden="true"
            />
          </button>
        </DropdownMenuTrigger>
        <DropdownMenuContent align="end">
          <DropdownMenuItem onClick={() => setEditDialogOpen(true)}>
            <span>Edit</span>
            <SquarePen />
          </DropdownMenuItem>
          <DropdownMenuItem
            variant="destructive"
            onClick={() => setDeleteDialogOpen(true)}
          >
            <span>Delete</span>
            <Trash2 />
          </DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>

      <AlertDialog open={deleteDialogOpen} onOpenChange={setDeleteDialogOpen}>
        <AlertDialogContent>
          <AlertDialogHeader>
            <AlertDialogTitle>Delete Staff Member?</AlertDialogTitle>
            <AlertDialogDescription>
              Are you sure you want to delete this staff member? This action
              cannot be undone.
            </AlertDialogDescription>
          </AlertDialogHeader>
          <AlertDialogFooter>
            <AlertDialogCancel>Cancel</AlertDialogCancel>
            <Button
              variant="destructive"
              disabled={deleteStaff.isPending}
              onClick={() => deleteStaff.mutate(staffMember.id)}
            >
              {deleteStaff.isPending ? (
                <LoaderCircle className="animate-spin" />
              ) : null}
              Delete Staff Member
            </Button>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialog>

      <DrawerDialog
        open={editDialogOpen}
        onOpenChange={setEditDialogOpen}
        title="Edit Staff Member"
        description="Update the details of this staff member."
      >
        <EditStaffForm staff={staffMember} setOpen={setEditDialogOpen} />
      </DrawerDialog>
    </li>
  );
}

function StaffList() {
  const { data: staff } = useStaff();

  if (staff.length === 0) {
    return (
      <div className="top-0 flex h-full w-full flex-1 flex-col items-center gap-6 rounded-lg p-8 text-center">
        <div className="flex flex-col items-center gap-2 pt-[26vh]">
          <p className="text-foreground text-xl font-medium">
            No staff members yet.
          </p>
          <p className="text-muted-foreground text-base">
            Get started by creating a new staff member.
          </p>
        </div>
      </div>
    );
  }

  return (
    <ul className="grid grid-cols-1 items-stretch gap-5 md:grid-cols-2 md:gap-4 lg:grid-cols-3">
      {staff.map((staffMember) => (
        <StaffItem key={staffMember.id} staffMember={staffMember} />
      ))}
    </ul>
  );
}

export default StaffList;
