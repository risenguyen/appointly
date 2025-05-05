import { createFileRoute } from "@tanstack/react-router";
import { Button } from "@/components/ui/button";
import {
  Drawer,
  DrawerTrigger,
  DrawerContent,
  DrawerTitle,
  DrawerDescription,
} from "@/components/ui/drawer";
import CreateTreatmentForm from "@/features/treatments/components/create-treatment-form";

export const Route = createFileRoute("/app/treatments")({
  component: RouteComponent,
});

function RouteComponent() {
  return (
    <div>
      <Drawer autoFocus>
        <DrawerTrigger asChild>
          <Button>Create treatment</Button>
        </DrawerTrigger>
        <DrawerContent>
          <div className="flex flex-col gap-4 overflow-y-auto px-5 py-8">
            <div className="flex flex-col gap-1.5">
              <DrawerTitle className="text-lg font-medium">
                Create treatment
              </DrawerTitle>
              <DrawerDescription>
                Fill in the details for the new treatment.
              </DrawerDescription>
            </div>

            <CreateTreatmentForm />
          </div>
        </DrawerContent>
      </Drawer>
    </div>
  );
}
