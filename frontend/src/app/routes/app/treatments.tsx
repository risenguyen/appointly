import { useState } from "react";
import { createFileRoute } from "@tanstack/react-router";

import { treatmentsQueryOptions } from "@/features/treatments/api/use-treatments";

import { Button } from "@/components/ui/button";
import DrawerDialog from "@/components/shared/drawer-dialog";

import CreateTreatmentForm from "@/features/treatments/components/create-treatment-form";
import TreatmentList from "@/features/treatments/components/treatment-list";

export const Route = createFileRoute("/app/treatments")({
  component: RouteComponent,
  loader: async ({ context: { queryClient } }) => {
    await queryClient.ensureQueryData(treatmentsQueryOptions());
  },
  pendingComponent: () => {
    return <div>Loading...</div>;
  },
});

function RouteComponent() {
  const [open, setOpen] = useState(false);

  return (
    <div className="flex min-h-full w-full flex-col gap-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-medium">Treatments</h1>
        <DrawerDialog
          open={open}
          onOpenChange={setOpen}
          trigger={<Button size="sm">Create Treatment</Button>}
          title="Create Treatment"
          description="
Fill in the details for the new treatment."
        >
          <CreateTreatmentForm setOpen={setOpen} />
        </DrawerDialog>
      </div>

      <div className="max-w-full flex-1">
        <TreatmentList />
      </div>
    </div>
  );
}
