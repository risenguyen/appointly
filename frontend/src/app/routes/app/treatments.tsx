import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/app/treatments')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/app/treatments"!</div>
}
