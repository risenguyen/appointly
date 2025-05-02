import { Menu } from "lucide-react";
import {
  Drawer,
  DrawerTrigger,
  DrawerContent,
  DrawerClose,
  DrawerDescription,
  DrawerFooter,
  DrawerHeader,
  DrawerTitle,
  DrawerOverlay,
  DrawerPortal,
} from "../ui/drawer";

function MobileNav() {
  return (
    <Drawer>
      <DrawerTrigger>
        <button
          className="flex cursor-pointer items-center pl-1"
          aria-label="menu"
          type="button"
        >
          <Menu className="leading-0" size="20px" />
        </button>
      </DrawerTrigger>
      <DrawerContent className="h-full"></DrawerContent>
    </Drawer>
  );
}

export default MobileNav;
