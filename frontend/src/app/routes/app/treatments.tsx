import { createFileRoute } from "@tanstack/react-router";
import { Button } from "@/components/ui/button";
import {
  Drawer,
  DrawerTrigger,
  DrawerClose,
  DrawerContent,
  DrawerHeader,
  DrawerFooter,
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
          <div className="flex flex-col overflow-y-auto">
            <DrawerHeader>
              <DrawerTitle className="text-lg font-medium">
                Create treatment
              </DrawerTitle>
              <DrawerDescription>
                Fill in the details for the new treatment.
              </DrawerDescription>
            </DrawerHeader>

            <CreateTreatmentForm />

            <DrawerFooter className="pt-2">
              <DrawerClose asChild>
                <Button variant="secondary">Cancel</Button>
              </DrawerClose>
            </DrawerFooter>
          </div>
        </DrawerContent>
      </Drawer>
    </div>
  );
}
