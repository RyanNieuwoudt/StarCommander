import * as React from "react";
import { Box, Heading } from "grommet";
import { Layout } from "components";
import { UpdateNameForm } from "components/player";

export default function Home() {
	return (
		<Layout>
			<Box align="center" pad="large">
				<Heading>Welcome, Commander.</Heading>
				<UpdateNameForm />
			</Box>
		</Layout>
	);
}
