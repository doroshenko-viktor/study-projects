import Head from 'next/head'
import Script from 'next/script'
import Link from 'next/link'
import Layout from '../components/layout'
import { getSortedPostsData } from '../lib/posts';
import utilStyles from '../styles/utils.module.css';
import Date from '../components/date';
import { GetStaticProps } from 'next';
import { HeadPostData } from '../types/post';

type Props = {
  posts: HeadPostData[],
};

export default function Home({ posts }: Props) {
  return (
    <Layout home>
      <Head>
        <title>Next.js Example application</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <Script
        src="https://connect.facebook.net/en_US/sdk.js"
        strategy="lazyOnload"
        onLoad={() =>
          console.log(`script loaded correctly, window.FB has been populated`)
        }
      />

      <main>
        <h1 className="title">
          Welcome to <a href="https://nextjs.org">Next.js!</a>
        </h1>

        <Link href="/posts/first-post">first post</Link>
      </main>

      <section className={`${utilStyles.headingMd} ${utilStyles.padding1px}`}>
        <h2 className={utilStyles.headingLg}>Blog</h2>
        <ul className={utilStyles.list}>
          {posts.map(({ id, date, title }) => (
            <li className={utilStyles.listItem} key={id}>
              <Link href={`/posts/${id}`}>
                <a>{title}</a>
              </Link>
              <br />
              <small className={utilStyles.lightText}>
                <Date dateString={date} />
              </small>
            </li>
          ))}
        </ul>
      </section>
    </Layout>
  )
}

export const getStaticProps: GetStaticProps = async () => {
  const posts = getSortedPostsData();
  return {
    props: {
      posts
    }
  };
}