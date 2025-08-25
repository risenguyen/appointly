import { useState } from "react";
import { toast } from "sonner";

import { useTreatments } from "../api/use-treatments";
import { useDeleteTreatment } from "../api/use-delete-treatment";
import type { TreatmentResponse } from "@/api";

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
import EditTreatmentForm from "./edit-treatment-form";

function TreatmentItem({ treatment }: { treatment: TreatmentResponse }) {
  const [deleteDialogOpen, setDeleteDialogOpen] = useState(false);
  const [editDialogOpen, setEditDialogOpen] = useState(false);

  const deleteTreatment = useDeleteTreatment({
    onSuccess() {
      toast.success("Treatment deleted successfully.");
      setDeleteDialogOpen(false);
    },
    onError(error) {
      toast.error(
        `Failed to delete treatment. ${error instanceof Error ? "Something went wrong." : `(${error.status})`}`,
      );
      setDeleteDialogOpen(false);
    },
  });

  return (
    <li
      key={treatment.id}
      className="bg-card-2 relative flex aspect-[1.9] w-full flex-col justify-between rounded-md p-6 md:aspect-[1.72] lg:aspect-[1.49] xl:aspect-[2] 2xl:aspect-[2.38]"
    >
      <div className="flex flex-col gap-0.5">
        <h1 className="text-base font-medium">{treatment.name}</h1>
        <p className="text-muted-foreground text-base break-words">
          {treatment.description}
        </p>
      </div>

      <div className="flex items-center justify-between">
        <span className="text-base">${treatment.price.toFixed(2)}</span>
        <span className="text-muted-foreground text-base">
          {treatment.durationInMinutes} min
        </span>
      </div>

      <DropdownMenu>
        <DropdownMenuTrigger asChild>
          <button
            aria-label="Open Menu"
            className="absolute top-6 right-6 focus:outline-none"
            type="button"
          >
            <Ellipsis className="cursor-pointer" size="16px" />
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
            <AlertDialogTitle>Delete Treatment?</AlertDialogTitle>
            <AlertDialogDescription>
              Are you sure you want to delete this treatment? This action cannot
              be undone.
            </AlertDialogDescription>
          </AlertDialogHeader>
          <AlertDialogFooter>
            <AlertDialogCancel>Cancel</AlertDialogCancel>
            <Button
              variant="destructive"
              disabled={deleteTreatment.isPending}
              onClick={() => deleteTreatment.mutate(treatment.id)}
            >
              {deleteTreatment.isPending ? (
                <LoaderCircle className="animate-spin" />
              ) : null}
              Delete Treatment
            </Button>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialog>

      <DrawerDialog
        open={editDialogOpen}
        onOpenChange={setEditDialogOpen}
        title="Edit Treatment"
        description="Update the details of this treatment."
      >
        <EditTreatmentForm treatment={treatment} setOpen={setEditDialogOpen} />
      </DrawerDialog>
    </li>
  );
}

type TreatmentTypeString = "Hair" | "Nails" | "Massages";

const treatmentTypeMap: Record<number, TreatmentTypeString> = {
  0: "Hair",
  1: "Nails",
  2: "Massages",
};

function TreatmentList() {
  const { data: treatments } = useTreatments();

  if (treatments.length === 0) {
    return (
      <div className="top-0 flex h-full w-full flex-1 flex-col items-center gap-6 rounded-lg p-8 text-center">
        <div className="flex flex-col items-center gap-2 pt-[26vh]">
          <p className="text-foreground text-xl font-medium">
            No treatments yet.
          </p>
          <p className="text-muted-foreground text-base">
            Get started by creating a new treatment.
          </p>
        </div>
      </div>
    );
  }

  return (
    <ul className="flex flex-col gap-8">
      {Object.entries(
        treatments.reduce(
          (acc, cur) => {
            const typeString = treatmentTypeMap[cur.treatmentType];
            acc[typeString].push(cur);
            return acc;
          },
          { Hair: [], Nails: [], Massages: [] } as Record<
            TreatmentTypeString,
            TreatmentResponse[]
          >,
        ),
      ).map(([category, treatments]) => (
        <li key={category} className="flex flex-col gap-2">
          <div className="text-xl font-medium">{category}</div>
          <ul className="grid grid-cols-1 items-stretch gap-4 md:grid-cols-2 lg:grid-cols-3">
            {treatments.map((treatment) => (
              <TreatmentItem key={treatment.id} treatment={treatment} />
            ))}
          </ul>
        </li>
      ))}
    </ul>
  );
}

export default TreatmentList;
