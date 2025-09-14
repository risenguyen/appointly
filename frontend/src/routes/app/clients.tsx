import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/app/clients')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/app/clients"!</div>
}
