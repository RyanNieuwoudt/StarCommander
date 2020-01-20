import * as React from "react";
import { Box, Heading } from "grommet";
import { Layout } from "components";
import { Helm } from "components/ship/helm";
import { Navigation } from "components/ship/navigation";

export default function Bridge() {
	return (
		<Layout>
			<Box align="center" pad="large">
				<Heading>Welcome, Commander.</Heading>
				<Helm />
				<Navigation />
			</Box>
		</Layout>
	);
}
