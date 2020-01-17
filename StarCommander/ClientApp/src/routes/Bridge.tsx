import * as React from "react";
import { Box, Heading } from "grommet";
import { Layout } from "components";
import { Helm } from "components/ship/helm";

export default function Bridge() {
	return (
		<Layout>
			<Box align="center" pad="large">
				<Heading>Welcome, Commander.</Heading>
				<Helm />
			</Box>
		</Layout>
	);
}
