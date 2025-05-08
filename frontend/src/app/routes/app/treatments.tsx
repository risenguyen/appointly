import { createFileRoute } from "@tanstack/react-router";
import { Button } from "@/components/ui/button";
import DrawerDialog from "@/components/shared/drawer-dialog";
import CreateTreatmentForm from "@/features/treatments/components/create-treatment-form";

export const Route = createFileRoute("/app/treatments")({
  component: RouteComponent,
});

function RouteComponent() {
  return (
    <div className="min-h-full w-full py-2">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-medium">Treatments</h1>
        <DrawerDialog
          trigger={<Button size="sm">Create treatment</Button>}
          title="Create treatment"
          description="
Fill in the details for the new treatment."
        >
          <CreateTreatmentForm />
        </DrawerDialog>
      </div>
    </div>
  );
}
