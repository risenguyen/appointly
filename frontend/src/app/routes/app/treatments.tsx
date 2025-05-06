import { createFileRoute } from "@tanstack/react-router";
import { Button } from "@/components/ui/button";
import DrawerDialog from "@/components/shared/drawer-dialog";
import CreateTreatmentForm from "@/features/treatments/components/create-treatment-form";

export const Route = createFileRoute("/app/treatments")({
  component: RouteComponent,
});

function RouteComponent() {
  return (
    <div className="flex justify-end px-8">
      <DrawerDialog
        trigger={<Button size="sm">Create treatment</Button>}
        title="Create treatment"
        description="
Fill in the details for the new treatment."
      >
        <CreateTreatmentForm />
      </DrawerDialog>
    </div>
  );
}
